/* eslint-disable indent */
import {
  ADD_USER,
  EDIT_USER,
  GET_USERS,
  DELETE_USER,
  SEARCH_USER
} from '@/constants/constants';
import { ADMIN_USERS } from '@/constants/routes';
import { displayActionMessage } from '@/helpers/utils';
import {
  all, call, put, select
} from 'redux-saga/effects';
import { setLoading, setRequestStatus } from '@/redux/actions/miscActions';
import { history } from '@/routers/AppRouter';
import firebase from '@/services/firebase';
import {
  addUserSuccess,
  clearSearchState,
  editUserSuccess, 
  getUsersSuccess,
  deleteUserSuccess,
  searchUserSuccess
} from '../actions/userActions';

function* initRequest() {
  yield put(setLoading(true));
  yield put(setRequestStatus(null));
}

function* handleError(e) {
  yield put(setLoading(false));
  yield put(setRequestStatus(e?.message || 'Failed to fetch users'));
  console.log('ERROR: ', e);
}

function* handleAction(location, message, status) {
  if (location) yield call(history.push, location);
  yield call(displayActionMessage, message, status);
}

function* userSaga({ type, payload }) {
  switch (type) {
    case GET_USERS:
      try {
        yield initRequest();
        const state = yield select();
        const result = yield call(firebase.getAllUsers, payload);

        if (!result) {
          handleError('No items found.');
        } else {
          yield put(getUsersSuccess({
            users: result.users,
            lastKey: result.lastKey ? result.lastKey : state.users.lastRefKey,
            total: result.total ? result.total : state.users.total
          }));
          yield put(setRequestStatus(''));
        }
        yield put(setLoading(false));
      } catch (e) {
        console.log(e);
        yield handleError(e);
      }
      break;

    case ADD_USER: {
      try {
        yield initRequest();

        const { imageCollection } = payload;
        const key = yield call(firebase.generateKey);
        const downloadURL = yield call(firebase.storeImage, key, 'users', payload.image);
        const image = { id: key, url: downloadURL };
        let images = [];

        if (imageCollection.length !== 0) {
          const imageKeys = yield all(imageCollection.map(() => firebase.generateKey));
          const imageUrls = yield all(imageCollection.map((img, i) => firebase.storeImage(imageKeys[i](), 'users', img.file)));
          images = imageUrls.map((url, i) => ({
            id: imageKeys[i](),
            url
          }));
        }

        const user = {
          ...payload,
          image: downloadURL,
          imageCollection: [image, ...images]
        };

        yield call(firebase.addUser, key, user);
        yield put(addUserSuccess({
          id: key,
          ...user
        }));
        yield handleAction(ADMIN_USERS, 'Item succesfully added', 'success');
        yield put(setLoading(false));
      } catch (e) {
        yield handleError(e);
        yield handleAction(undefined, `Item failed to add: ${e?.message}`, 'error');
      }
      break;
    }
    case EDIT_USER: {
      try {
        yield initRequest();

        const { image, imageCollection } = payload.updates;
        let newUpdates = { ...payload.updates };

        if (image.constructor === File && typeof image === 'object') {
          try {
            yield call(firebase.deleteUserImage, payload.id);
          } catch (e) {
            console.error('Failed to delete image ', e);
          }

          const url = yield call(firebase.storeImage, payload.id, 'users', image);
          newUpdates = { ...newUpdates, image: url };
        }

        yield call(firebase.editUser, payload.id, newUpdates);
        yield put(editUserSuccess({
          id: payload.id,
          updates: newUpdates
        }));
        yield handleAction(ADMIN_USERS, 'Item succesfully edited', 'success');
        yield put(setLoading(false));
      } catch (e) {
        yield handleError(e);
        yield handleAction(undefined, `Item failed to edit: ${e.message}`, 'error');
      }
      break;
    }
    case DELETE_USER: {
      try {
        yield initRequest();
        yield call(firebase.removeUser, payload);
        yield put(deleteUserSuccess(payload));
        yield put(setLoading(false));
        yield handleAction(ADMIN_USERS, 'Item succesfully removed', 'success');
      } catch (e) {
        yield handleError(e);
        yield handleAction(undefined, `Item failed to remove: ${e.message}`, 'error');
      }
      break;
    }
    case SEARCH_USER: {
      try {
        yield initRequest();
        // clear search data
        yield put(clearSearchState());

        const state = yield select();
        const result = yield call(firebase.searchUsers, payload.searchKey);

        if (result.users.length === 0) {
          yield handleError({ message: 'No user found.' });
          yield put(clearSearchState());
        } else {
          yield put(searchUserSuccess({
            users: result.users,
            lastKey: result.lastKey ? result.lastKey : state.users.searchedUsers.lastRefKey,
            total: result.total ? result.total : state.users.searchedUsers.total
          }));
          yield put(setRequestStatus(''));
        }
        yield put(setLoading(false));
      } catch (e) {
        yield handleError(e);
      }
      break;
    }
    default: {
      throw new Error(`Unexpected action type ${type}`);
    }
  }
}

export default userSaga;

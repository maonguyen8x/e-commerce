import {
  ADD_USER,
  ADD_USER_SUCCESS,
  DELETE_USER,
  DELETE_USER_SUCCESS,
  EDIT_USER,
  EDIT_USER_SUCCESS, 
  GET_USERS,
  GET_USERS_BY_UID,
  GET_USERS_SUCCESS,
  REGISTER_USER,
  SEARCH_USER,
  SEARCH_USER_SUCCESS,
  CLEAR_SEARCH_STATE
} from '@/constants/constants';

// insert in profile array
export const registerUser = (user) => ({
  type: REGISTER_USER,
  payload: user
});

export const addUserSuccess = (user) => ({
  type: ADD_USER_SUCCESS,
  payload: user
});

export const getAllUsers = (lastRef) => ({
  type: GET_USERS,
  payload: lastRef
});

export const getUsersSuccess = (users) => ({
  type: GET_USERS_SUCCESS,
  payload: users
});

// different from registerUser -- only inserted in admins' users array not in profile array
export const addUser = (user) => ({
  type: ADD_USER,
  payload: user
});

export const searchUser = (searchKey) => ({
  type: SEARCH_USER,
  payload: {
    searchKey
  }
});

export const editUser = (updates) => ({
  type: EDIT_USER,
  payload: updates
});

export const editUserSuccess = (updates) => ({
  type: EDIT_USER_SUCCESS,
  payload: updates
});

export const deleteUser = (id) => ({
  type: DELETE_USER,
  payload: id
});

export const deleteUserSuccess = (id) => ({
  type: DELETE_USER_SUCCESS,
  payload: id
});

export const searchUserSuccess = (users) => ({
  type: SEARCH_USER_SUCCESS,
  payload: users
});

export const clearSearchState = () => ({
  type: CLEAR_SEARCH_STATE
});

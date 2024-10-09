import { ImageLoader } from '@/components/common';
import { EDIT_USER } from '@/constants/routes';
import { displayActionMessage, displayDate, displayMoney } from '@/helpers/utils';
import PropType from 'prop-types';
import React, { useRef } from 'react';
import Skeleton, { SkeletonTheme } from 'react-loading-skeleton';
import { useDispatch } from 'react-redux';
import { useHistory, withRouter } from 'react-router-dom';
import { deleteUser } from '@/redux/actions/userActions';
import { useTranslation } from 'react-i18next';

const UserItem = ({ user }) => {
  const { t } = useTranslation();

  const dispatch = useDispatch();
  const history = useHistory();
  const userRef = useRef(null);

  const onClickEdit = () => {
    history.push(`${EDIT_USER}/${user.id}`);
  };

  const onDeleteUser = () => {
    userRef.current.classList.toggle('item-active');
  };

  const onConfirmDelete = () => {
    dispatch(deleteUser(user.id));
    displayActionMessage('Item successfully deleted');
    userRef.current.classList.remove('item-active');
  };

  const onCancelDelete = () => {
    userRef.current.classList.remove('item-active');
  };

  return (
    <SkeletonTheme
      color="#e1e1e1"
      highlightColor="#f2f2f2"
    >
      <div
        className={`item item-products ${!user.id && 'item-loading'}`}
        ref={userRef}
      >
        <div className="grid grid-count-6">
          <div className="grid-col item-img-wrapper">
            {user.avatar ? (
              <ImageLoader
                alt={user.fullname}
                className="item-img"
                src={user.avatar}
              />
            ) : <Skeleton width={50} height={30} />}
          </div>
          <div className="grid-col">
            <span className="text-overflow-ellipsis">{user.fullname || <Skeleton width={50} />}</span>
          </div>
          <div className="grid-col">
            <span>{user.email || <Skeleton width={50} />}</span>
          </div>
          <div className="grid-col">
            <span>{user.address || <Skeleton width={50} />}</span>
          </div>
          <div className="grid-col">
            <span>
              {user.dateJoined ? displayDate(user.dateJoined) : <Skeleton width={30} />}
            </span>
          </div>
          <div className="grid-col">
            <span>{user.role || <Skeleton width={50} />}</span>
          </div>
        </div>
        {user.id && (
          <div className="item-action">
            <button
              className="button button-border button-small"
              onClick={onClickEdit}
              type="button"
            >
              {t('edit')}
            </button>
            &nbsp;
            <button
              className="button button-border button-small button-danger"
              onClick={onDeleteUser}
              type="button"
            >
              {t('delete')}
            </button>
            <div className="item-action-confirm">
              <h5>{t('are_you_sure_you_want_to_delete_this')}</h5>
              <button
                className="button button-small button-border"
                onClick={onCancelDelete}
                type="button"
              >
                {t('no')}
              </button>
              &nbsp;
              <button
                className="button button-small button-danger"
                onClick={onConfirmDelete}
                type="button"
              >
                {t('yes')}
              </button>
            </div>
          </div>
        )}
      </div>
    </SkeletonTheme>
  );
};

UserItem.propTypes = {
  user: PropType.shape({
    id: PropType.string,
    avatar: PropType.string,
    fullname: PropType.string,
    email: PropType.string,
    address: PropType.string,
    role: PropType.string,
    banner: PropType.string,
    keywords: PropType.arrayOf(PropType.string),
    dateAdded: PropType.number,
  }).isRequired
};

export default withRouter(UserItem);

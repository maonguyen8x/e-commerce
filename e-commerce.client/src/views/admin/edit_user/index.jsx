import { LoadingOutlined } from '@ant-design/icons';
import { useDocumentTitle, useUser, useScrollTop } from '@/hooks';
import PropType from 'prop-types';
import React, { lazy, Suspense } from 'react';
import { useDispatch } from 'react-redux';
import { Redirect, withRouter } from 'react-router-dom';
import { editUser } from '@/redux/actions/userActions';
import { useTranslation } from 'react-i18next';

const UserForm = lazy(() => import('../components/UserForm'));

const EditUser = ({ match }) => {
  const { t } = useTranslation();

  useDocumentTitle('Edit User | ITAGE SHOP');
  useScrollTop();
  const { user, error, isLoading } = useUser(match.params.id);
  const dispatch = useDispatch();

  const onSubmitForm = (updates) => {
    dispatch(editUser(user.id, updates));
  };

  return (
    <div className="product-form-container">
      {error && <Redirect to="/dashboard/users" />}
      <h2>{t('edit_user')}</h2>
      {user && (
        <Suspense fallback={(
          <div className="loader" style={{ minHeight: '80vh' }}>
            <h6>{t('loading')}</h6>
            <br />
            <LoadingOutlined />
          </div>
        )}
        >
          <UserForm
            isLoading={isLoading}
            onSubmit={onSubmitForm}
            user={user}
          />
        </Suspense>
      )}
    </div>
  );
};

EditUser.propTypes = {
  match: PropType.shape({
    params: PropType.shape({
      id: PropType.string
    })
  }).isRequired
};

export default withRouter(EditUser);

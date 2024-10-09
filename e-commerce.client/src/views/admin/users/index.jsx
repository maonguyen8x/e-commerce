/* eslint-disable react/jsx-props-no-spreading */
import { Boundary } from '@/components/common';
import { AppliedFilters, UserList } from '@/components/user';
import { useDocumentTitle, useScrollTop } from '@/hooks';
import React from 'react';
import { useSelector } from 'react-redux';
import { withRouter } from 'react-router-dom';
import { selectFilter } from '@/selectors/selectorUser';
import { UsersNavbar } from '../components';
import UsersTable from '../components/UsersTable';

const Users = () => {
  useDocumentTitle('User List | ITAGE SHOP Admin');
  useScrollTop();

  const store = useSelector((state) => ({
    filteredUsers: selectFilter(state.users.items, state.filter),
    requestStatus: state.app.requestStatus,
    isLoading: state.app.loading,
    users: state.users
  }));

  return (
    <Boundary>
      <UsersNavbar
        usersCount={store.users.items.length}
        totalUsersCount={store.users.total}
      />
      <div className="product-admin-items">
        <UserList {...store}>
          <AppliedFilters filter={store.filter} />
          <UsersTable filteredUsers={store.filteredUsers} />
        </UserList>
      </div>
    </Boundary>
  );
};

export default withRouter(Users);

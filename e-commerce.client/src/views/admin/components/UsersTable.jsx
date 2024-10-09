/* eslint-disable react/forbid-prop-types */
import PropType from 'prop-types';
import React from 'react';
import { UserItem } from '.';
import { useTranslation } from 'react-i18next';

const UsersTable = ({ filteredUsers }) => {
  const { t } = useTranslation();

  return (
  <div>
    {filteredUsers.length > 0 && (
      <div className="grid grid-product grid-count-6">
        <div className="grid-col" />
        <div className="grid-col">
          <h5>{t('full_name')}</h5>
        </div>
        <div className="grid-col">
          <h5>{t('email')}</h5>
        </div>
        <div className="grid-col">
          <h5>{t('address')}</h5>
        </div>
        <div className="grid-col">
          <h5>{t('date_added')}</h5>
        </div>
        <div className="grid-col">
          <h5>{t('role')}</h5>
        </div>
      </div>
    )}
    {filteredUsers.length === 0 ? new Array(10).fill({}).map((user, index) => (
      <UserItem
        // eslint-disable-next-line react/no-array-index-key
        key={`user-skeleton ${index}`}
        user={user}
      />
    )) : filteredUsers.map((user) => (
      <UserItem
        key={user.id}
        user={user}
      />
    ))}
  </div>
  );
};

UsersTable.propTypes = {
  filteredUsers: PropType.array.isRequired
};

export default UsersTable;

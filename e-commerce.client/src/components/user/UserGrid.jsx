import { useBasket } from '@/hooks';
import PropType from 'prop-types';
import React from 'react';
import UserItem from './UserItem';

const UserGrid = ({ users }) => {
  const { addToBasket, isItemOnBasket } = useBasket();

  return (
    <div className="product-grid">
      {users.length === 0 ? new Array(12).fill({}).map((user, index) => (
        <UserItem
          // eslint-disable-next-line react/no-array-index-key
          key={`product-skeleton ${index}`}
          user={user}
        />
      )) : users.map((user) => (
        <UserItem
          key={user.id}
          isItemOnBasket={isItemOnBasket}
          addToBasket={addToBasket}
          user={user}
        />
      ))}
    </div>
  );
};

UserGrid.propTypes = {
  // eslint-disable-next-line react/forbid-prop-types
  users: PropType.array.isRequired
};

export default UserGrid;

/* eslint-disable react/forbid-prop-types */
import { Boundary, MessageDisplay } from '@/components/common';
import PropType from 'prop-types';
import React, { useEffect, useState } from 'react';
import { useDispatch } from 'react-redux';
import { setLoading } from '@/redux/actions/miscActions';
import { getAllUsers } from '@/redux/actions/userActions';
import firebase from '@/services/firebase';

const UserList = (props) => {
  const {
    users,
    filteredUsers,
    isLoading,
    requestStatus,
    children
  } = props;
  const [isFetching, setFetching] = useState(false);
  const dispatch = useDispatch();
  const [error, setError] = useState(null);

  // Flag to check if the component is still mounted
  let isMounted = true;

  const fetchUsers = () => {
    setFetching(true);
    dispatch(getAllUsers(users.lastRefKey));
  };

    useEffect(() => {
      const fetchUsers = async () => {
        try {
            const usersData = await firebase.getAllUsers();
            if (isMounted) {
              // Update the state only if the component is still mounted
              setUsers(usersData);
              setLoading(false);
            }
        } catch (err) {
            if (isMounted) {
              setLoading(false);
            }
            setError(err.message);
        } finally {
            setLoading(false);
        }
      };
    
    fetchUsers();
    return () => {
      isMounted = false;  // Set flag to false when component unmounts
    };
  }, []);

  useEffect(() => {
    if (users.items.length === 0 || !users.lastRefKey) {
      fetchUsers();
    }

    window.scrollTo(0, 0);
    return () => dispatch(setLoading(false));
  }, []);

  useEffect(() => {
    setFetching(false);
  }, [users.lastRefKey]);

  if (filteredUsers.length === 0 && !isLoading) {
    return (
      <MessageDisplay message={requestStatus?.message || 'No users found.'} />
    );
  } if (filteredUsers.length === 0 && requestStatus) {
    return (
      <MessageDisplay
        message={requestStatus?.message || 'Something went wrong :('}
        action={fetchUsers}
        buttonLabel="Try Again"
      />
    );
  }
  return (
    <Boundary>
      {children}
      {users.items.length < users.total && (
        <div className="d-flex-center padding-l">
          <button
            className="button button-small"
            disabled={isFetching}
            onClick={fetchUsers}
            type="button"
          >
            {isFetching ? 'Fetching Items...' : 'Show More Items'}
          </button>
        </div>
      )}
    </Boundary>
  );
};

UserList.defaultProps = {
  requestStatus: null
};

UserList.propTypes = {
  users: PropType.object.isRequired,
  filteredUsers: PropType.array.isRequired,
  isLoading: PropType.bool.isRequired,
  requestStatus: PropType.string,
  children: PropType.oneOfType([
    PropType.arrayOf(PropType.node),
    PropType.node
  ]).isRequired
};

export default UserList;

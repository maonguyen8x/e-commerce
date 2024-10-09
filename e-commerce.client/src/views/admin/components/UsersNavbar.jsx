import { FilterOutlined, PlusOutlined } from '@ant-design/icons';
import { FiltersToggle, SearchBar } from '@/components/common';
import { ADMIN_USERS } from '@/constants/routes';
import PropType from 'prop-types';
import React from 'react';
import { useHistory } from 'react-router-dom';
import { useTranslation } from 'react-i18next';

const UsersNavbar = (props) => {
  const { i18n, t } = useTranslation()

  const { usersCount, totalUsersCount} = props;
  const history = useHistory();

  return (
    <div className="product-admin-header">
      <h3 className="product-admin-header-title">
        {t('users')} &nbsp;
        (
        {`${usersCount} / ${totalUsersCount}`}
        )
      </h3>
      <SearchBar />
            &nbsp;
      <FiltersToggle>
        <button className="button-muted button-small" type="button">
          <FilterOutlined />
          &nbsp;{t('more_filters')}
        </button>
      </FiltersToggle>
      <button
        className="button button-small"
        onClick={() => history.push(ADMIN_USERS)}
        type="button"
      >
        <PlusOutlined />
        &nbsp; {t('add_new_user')}
      </button>
    </div>
  );
};

UsersNavbar.propTypes = {
  usersCount: PropType.number.isRequired,
  totalUsersCount: PropType.number.isRequired
};

export default UsersNavbar;

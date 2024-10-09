import { ADMIN_PRODUCTS } from '@/constants/routes';
import { ADMIN_USERS } from '@/constants/routes';
import React from 'react';
import { NavLink } from 'react-router-dom';
import { useTranslation } from 'react-i18next';

const SideNavigation = () => {
  const { t } = useTranslation();

  return (
    <aside className="sidenavigation">
      <div className="sidenavigation-wrapper">
        <div className="sidenavigation-item">
          <NavLink
            activeClassName="sidenavigation-menu-active"
            className="sidenavigation-menu"
            to={ADMIN_PRODUCTS}
          >
            {t('products')}
          </NavLink>
        </div>
        <div className="sidenavigation-item">
          <NavLink
              activeClassName="sidenavigation-menu-active"
              className="sidenavigation-menu"
              to={ADMIN_USERS}
            >
            {t('users')}
          </NavLink>
          {/* <h4 className="sidenavigation-menu my-0">{t('users')}</h4> */}
        </div>
      </div>
    </aside>
  );
};

export default SideNavigation;

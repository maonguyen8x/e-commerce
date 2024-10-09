import { useDocumentTitle, useScrollTop } from '@/hooks';
import React from 'react';
import { useTranslation } from 'react-i18next';

const Dashboard = () => {
  const { t } = useTranslation();
  
  useDocumentTitle('Welcome | Admin Dashboard');
  useScrollTop();

  return (
    <div className="loader">
      <h2>{t('welcome_to_admin_dashboard')}</h2>
    </div>
  );
};

export default Dashboard;

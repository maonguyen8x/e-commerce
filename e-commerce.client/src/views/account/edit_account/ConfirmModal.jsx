import { CheckOutlined, CloseOutlined } from '@ant-design/icons';
import { Modal } from '@/components/common';
import { useFormikContext } from 'formik';
import PropType from 'prop-types';
import React, { useState } from 'react';
import { useTranslation } from 'react-i18next';

const ConfirmModal = ({ onConfirmUpdate, modal }) => {
  const { t } = useTranslation();

  const [password, setPassword] = useState('');
  const { values } = useFormikContext();

  return (
    <Modal
      isOpen={modal.isOpenModal}
      onRequestClose={modal.onCloseModal}
    >
      <div className="text-center padding-l">
        <h4>{t('confirm_update')}</h4>
        <p>
          {t('freetext1')} &nbsp;
          <strong>{t('email')}</strong>
          ,
          <br />
          {t('confirm_password')}
        </p>
        <input
          className="input-form d-block"
          onChange={(e) => setPassword(e.target.value)}
          placeholder= {t('enter_your_password')}
          required
          type="password"
          value={password}
        />
      </div>
      <br />
      <div className="d-flex-center">
        <button
          className="button"
          onClick={() => {
            onConfirmUpdate(values, password);
            modal.onCloseModal();
          }}
          type="button"
        >
          <CheckOutlined />
          &nbsp;
          {t('confirm')}
        </button>
      </div>
      <button
        className="modal-close-button button button-border button-border-gray button-small"
        onClick={modal.onCloseModal}
        type="button"
      >
        <CloseOutlined />
      </button>
    </Modal>
  );
};

ConfirmModal.propTypes = {
  onConfirmUpdate: PropType.func.isRequired,
  modal: PropType.shape({
    onCloseModal: PropType.func,
    isOpenModal: PropType.bool
  }).isRequired
};

export default ConfirmModal;

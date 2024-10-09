/* eslint-disable jsx-a11y/label-has-associated-control */
import { CheckOutlined, LoadingOutlined } from '@ant-design/icons';
import { ImageLoader } from '@/components/common';
import {
  CustomColorInput, CustomCreatableSelect, CustomInput, CustomTextarea
} from '@/components/formik';
import {
  Field, FieldArray, Form, Formik
} from 'formik';
import { useFileHandler } from '@/hooks';
import PropType from 'prop-types';
import React from 'react';
import * as Yup from 'yup';
import { useTranslation } from 'react-i18next';

const FormSchema = Yup.object().shape({
  fullname: Yup.string()
    .required('Product name is required.')
    .max(60, 'Product name must only be less than 60 characters.'),
  address: Yup.string()
    .required('Address is required.'),
  mobile: Yup.number()
    .positive('Mobile is invalid.')
    .integer('Mobile should be an integer.')
    .required('Mobile is required.'),
  role: Yup.string()
    .required('Role is required.'),
  keywords: Yup.array()
    .of(Yup.string())
    .min(1, 'Please enter at least 1 keyword for this user.'),
});

const UserForm = ({ user, onSubmit, isLoading }) => {
  const { t } = useTranslation();

  const initFormikValues = {
    fullname: user?.name || '',
    address: user?.address || '',
    avatar: user?.avatar || '',
    keywords: user?.keywords || [],
    banner: user?.banner || [],
    mobile: user?.mobile || '',
    role: user?.role || ''
  };

  const {
    imageFile,
    isFileLoading,
    onFileChange,
    removeImage
  } = useFileHandler({ avatar: {}});

  const onSubmitForm = (form) => {
    if (imageFile.avatar.file || user.imageUrl) {
      onSubmit({
        ...form,
        quantity: 1,
        // due to firebase function billing policy, let's add lowercase version
        // of name here instead in firebase functions
        dateJoined: new Date().getTime(),
        avatar: imageFile?.avatar?.file || user.imageUrl,
      });
    } else {
      // eslint-disable-next-line no-alert
      alert('User avatar is required.');
    }
  };

  return (
    <div>
      <Formik
        initialValues={initFormikValues}
        validateOnChange
        validationSchema={FormSchema}
        onSubmit={onSubmitForm}
      >
        {({ values, setValues }) => (
          <Form className="product-form">
            <div className="product-form-inputs">
              <div className="d-flex">
                <div className="product-form-field">
                  <Field
                    disabled={isLoading}
                    name="fullname"
                    type="text"
                    label= {t('full_name')}
                    placeholder={t('full_name')}
                    style={{ textTransform: 'capitalize' }}
                    component={CustomInput}
                  />
                </div>
                &nbsp;
              </div>
              <div className="product-form-field">
                <Field
                  disabled={disabled}
                  name="email"
                  id="email"
                  rows={3}
                  label= {t('email')}
                  placeholder={t("email_address")}
                  component={CustomTextarea}
                />
              </div>
              <div className="d-flex">
                <div className="product-form-field">
                  <Field
                    disabled={isLoading}
                    name="address"
                    id="address"
                    label="* address"
                    placeholder={t("address")}
                    component={CustomInput}
                  />
                </div>
              </div>
              <div className="d-flex">
                <div className="product-form-field">
                  <CustomCreatableSelect
                    defaultValue={values.keywords.map((key) => ({ value: key, label: key }))}
                    name="keywords"
                    iid="keywords"
                    isMulti
                    disabled={isLoading}
                    placeholder={t("create_select_keywords")}
                    label="* Keywords"
                  />
                </div>
              </div>
              <div className="product-form-field">
                <FieldArray
                  name="availableColors"
                  disabled={isLoading}
                  component={CustomColorInput}
                />
              </div>
              <br />
              <div className="product-form-field product-form-submit">
                <button
                  className="button"
                  disabled={isLoading}
                  type="submit"
                >
                  {isLoading ? <LoadingOutlined /> : <CheckOutlined />}
                  &nbsp;
                  {isLoading ? 'Saving User' : 'Save User'}
                </button>
              </div>
            </div>
            <div className="product-form-file">
              <div className="product-form-field">
                <span className="d-block padding-s">* {t('thumbnail')}</span>
                {!isFileLoading && (
                  <label htmlFor="product-input-file">
                    <input
                      disabled={isLoading}
                      hidden
                      id="product-input-file"
                      onChange={(e) => onFileChange(e, { name: 'image', type: 'single' })}
                      readOnly={isLoading}
                      type="file"
                    />
                    {t('choose_image')}
                  </label>
                )}
              </div>
              <div className="product-form-image-wrapper">
                {(imageFile.avatar.url || user.avatar) && (
                  <ImageLoader
                    alt=""
                    className="product-form-image-preview"
                    src={imageFile.avatar.url || user.avatar}
                  />
                )}
              </div>
            </div>
          </Form>
        )}
      </Formik>
    </div>
  );
};

UserForm.propTypes = {
  user: PropType.shape({
    fullname: PropType.string,
    address: PropType.string,
    avatar: PropType.string,
    banner: PropType.string,
    email: PropType.string,
    mobile: PropType.string,
    role: PropType.string,
    keywords: PropType.arrayOf(PropType.string),
  }).isRequired,
  onSubmit: PropType.func.isRequired,
  isLoading: PropType.bool.isRequired
};

export default UserForm;

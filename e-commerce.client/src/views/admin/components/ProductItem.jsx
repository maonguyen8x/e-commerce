import { ImageLoader } from '@/components/common';
import { EDIT_PRODUCT } from '@/constants/routes';
import { displayActionMessage, displayDate, displayMoney } from '@/helpers/utils';
import PropType from 'prop-types';
import React, { useRef } from 'react';
import Skeleton, { SkeletonTheme } from 'react-loading-skeleton';
import { useDispatch } from 'react-redux';
import { useHistory, withRouter } from 'react-router-dom';
import { removeProduct } from '@/redux/actions/productActions';
import { useTranslation } from 'react-i18next';

const ProductItem = ({ product }) => {
  const { t } = useTranslation();

  const dispatch = useDispatch();
  const history = useHistory();
  const productRef = useRef(null);

  const onClickEdit = () => {
    history.push(`${EDIT_PRODUCT}/${product.id}`);
  };

  const onDeleteProduct = () => {
    productRef.current.classList.toggle('item-active');
  };

  const onConfirmDelete = () => {
    dispatch(removeProduct(product.id));
    displayActionMessage('Item successfully deleted');
    productRef.current.classList.remove('item-active');
  };

  const onCancelDelete = () => {
    productRef.current.classList.remove('item-active');
  };

  return (
    <SkeletonTheme
      color="#e1e1e1"
      highlightColor="#f2f2f2"
    >
      <div
        className={`item item-products ${!product.id && 'item-loading'}`}
        ref={productRef}
      >
        <div className="grid grid-count-6">
          <div className="grid-col item-img-wrapper">
            {product.image ? (
              <ImageLoader
                alt={product.name}
                className="item-img"
                src={product.image}
              />
            ) : <Skeleton width={50} height={30} />}
          </div>
          <div className="grid-col">
            <span className="text-overflow-ellipsis">{product.name || <Skeleton width={50} />}</span>
          </div>
          <div className="grid-col">
            <span>{product.brand || <Skeleton width={50} />}</span>
          </div>
          <div className="grid-col">
            <span>{product.price ? displayMoney(product.price) : <Skeleton width={30} />}</span>
          </div>
          <div className="grid-col">
            <span>
              {product.dateAdded ? displayDate(product.dateAdded) : <Skeleton width={30} />}
            </span>
          </div>
          <div className="grid-col">
            <span>{product.maxQuantity || <Skeleton width={20} />}</span>
          </div>
        </div>
        {product.id && (
          <div className="item-action">
            <button
              className="button button-border button-small"
              onClick={onClickEdit}
              type="button"
            >
              {t('edit')}
            </button>
            &nbsp;
            <button
              className="button button-border button-small button-danger"
              onClick={onDeleteProduct}
              type="button"
            >
              {t('delete')}
            </button>
            <div className="item-action-confirm">
              <h5>{t('are_you_sure_you_want_to_delete_this')}</h5>
              <button
                className="button button-small button-border"
                onClick={onCancelDelete}
                type="button"
              >
                {t('no')}
              </button>
              &nbsp;
              <button
                className="button button-small button-danger"
                onClick={onConfirmDelete}
                type="button"
              >
                {t('yes')}
              </button>
            </div>
          </div>
        )}
      </div>
    </SkeletonTheme>
  );
};

ProductItem.propTypes = {
  product: PropType.shape({
    id: PropType.string,
    name: PropType.string,
    brand: PropType.string,
    price: PropType.number,
    maxQuantity: PropType.number,
    description: PropType.string,
    keywords: PropType.arrayOf(PropType.string),
    imageCollection: PropType.arrayOf(PropType.object),
    sizes: PropType.arrayOf(PropType.string),
    image: PropType.string,
    imageUrl: PropType.string,
    isFeatured: PropType.bool,
    isRecommended: PropType.bool,
    dateAdded: PropType.number,
    availableColors: PropType.arrayOf(PropType.string)
  }).isRequired
};

export default withRouter(ProductItem);

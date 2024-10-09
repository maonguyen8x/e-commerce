import authReducer from './authReducer';
import basketReducer from './basketReducer';
import checkoutReducer from './checkoutReducer';
import filterReducer from './filterReducer';
import miscReducer from './miscReducer';
import productReducer from './productReducer';
import profileReducer from './profileReducer';
import userReducer from './userReducer';

const rootReducer = {
  products: productReducer,
  users: userReducer,
  basket: basketReducer,
  auth: authReducer,
  profile: profileReducer,
  filter: filterReducer,
  checkout: checkoutReducer,
  app: miscReducer
};

export default rootReducer;

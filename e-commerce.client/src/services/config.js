
import * as firebase from 'firebase'

import 'firebase/storage';
import 'firebase/firestore';
import 'firebase/auth';

const firebaseConfig = {
  // apiKey: import.meta.env.VITE_FIREBASE_API_KEY,
  // authDomain: import.meta.env.VITE_FIREBASE_AUTH_DOMAIN,
  // databaseURL: import.meta.env.VITE_FIREBASE_DB_URL,
  // projectId: import.meta.env.VITE_FIREBASE_PROJECT_ID,
  // storageBucket: import.meta.env.VITE_FIREBASE_STORAGE_BUCKET,
  // messagingSenderId: import.meta.env.VITE_FIREBASE_MSG_SENDER_ID,
  // appId: import.meta.env.VITE_FIREBASE_APP_ID,
  // measurementId: import.meta.env.VITE_FIREBASE_MEASUREMENT_ID,

  apiKey: "AIzaSyBowXQrzJlAG5J0KIXw7ALQ58xERco6K5A",
  authDomain: "e-commerce-f3cae.firebaseapp.com",
  databaseURL: "https://e-commerce-f3cae-default-rtdb.asia-southeast1.firebasedatabase.app",
  projectId: "e-commerce-f3cae",
  storageBucket: "e-commerce-f3cae.appspot.com",
  messagingSenderId: "379267657937",
  appId: "1=379267657937=web=886fb2b2ac4fb773abd5f0",
  measurementId: "G-5ZF6EL79MW"
};

export default firebaseConfig;

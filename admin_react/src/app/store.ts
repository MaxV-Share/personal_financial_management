import createSagaMiddleware from "@redux-saga/core";
import {
  Action,
  combineReducers,
  configureStore,
  ThunkAction,
} from "@reduxjs/toolkit";
import { createBrowserHistory } from "history";
import currencyReducer from "src/features/Currency/currencySlice";
import paymentAccountReducer from "src/features/PaymentAccount/paymentAccountSlice";
import transactionCategoryReducer from "src/features/TransactionCategory/transactionCategorySlice";
import transactionReducer from "src/features/Transactions/transactionSlice";
import paymentAccountTypeReducer from "../features/PaymentAccountType/paymentAccountTypeSlice";
import globalReducer from "./globalSlice";
import rootSaga from "./rootsaga";

const rootReducer = combineReducers({
  //   router: connectRouter(history),
  global: globalReducer,
  currency: currencyReducer,
  paymentAccount: paymentAccountReducer,
  transactionCategory: transactionCategoryReducer,
  paymentAccountType: paymentAccountTypeReducer,
  transaction: transactionReducer,
});
export const history = createBrowserHistory();

const sagaMiddleware = createSagaMiddleware();
export const store = configureStore({
  reducer: rootReducer,
  middleware: (getDefaultMiddleware) =>
    getDefaultMiddleware().concat(sagaMiddleware),
});
sagaMiddleware.run(rootSaga);

export type AppDispatch = typeof store.dispatch;
export type RootState = ReturnType<typeof store.getState>;
export type AppThunk<ReturnType = void> = ThunkAction<
  ReturnType,
  RootState,
  unknown,
  Action<string>
>;

import { all } from "redux-saga/effects";
import currencySaga from "src/features/Currency/currencySaga";
import paymentAccountTypeSaga from "src/features/PaymentAccountType/paymentAccountTypeSaga";
import transactionCategorySaga from "src/features/TransactionCategory/transactionCategorySaga";

function* helloSaga() {
  console.log("hello saga");
  yield 1;
}

export default function* rootSaga() {
  yield all([
    helloSaga(),
    currencySaga(),
    transactionCategorySaga(),
    paymentAccountTypeSaga(),
  ]);
}

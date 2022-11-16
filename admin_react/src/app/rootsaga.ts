import { all } from "redux-saga/effects";
import currencySaga from "src/features/Currency/currencySaga";
import paymentAccountSaga from "src/features/PaymentAccount/paymentAccountSaga";
import paymentAccountTypeSaga from "src/features/PaymentAccountType/paymentAccountTypeSaga";
import transactionCategorySaga from "src/features/TransactionCategory/transactionCategorySaga";
import transactionSaga from "src/features/Transactions/transactionSaga";

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
    paymentAccountSaga(),
    transactionSaga(),
  ]);
}

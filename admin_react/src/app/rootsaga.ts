import { all } from "redux-saga/effects";

function* helloSaga() {
  console.log("hello saga");
  yield 1;
}

export default function* rootSaga() {
  yield all([helloSaga()]);
}

import { PayloadAction } from "@reduxjs/toolkit";
import { put, takeLatest } from "redux-saga/effects";
import { IBasePaging, IFilterBodyRequest } from "src/models/Bases";
import { IPaymentAccountModel } from "src/models/PaymentAccount";
import { paymentAccountActions } from "./paymentAccountSlice";

function* fetchPaymentAccounts(action: PayloadAction<IFilterBodyRequest>) {
  try {
    // const res: IBasePaging<IPaymentAccountModel> = yield call(
    //   paymentAccountApi.getAll,
    //   action.payload
    // );
    const res: IBasePaging<IPaymentAccountModel> = {
      data: [
        {
          id: "id",
          name: "tiền mặt",
          isReport: false,
          description: "Description",
          initialMoney: 1000000,
          type: {
            id: "id",
            code: "code",
            name: "type Name",
          },
          currency: {
            code: "VND",
            name: "name",
            icon: "",
            id: "id",
          },
        },
        {
          id: "id1",
          name: "name",
          isReport: false,
          description: "Description",
          initialMoney: 10000000,
          type: {
            id: "id",
            code: "code",
            name: "type Name",
          },
          currency: {
            code: "VND",
            name: "name",
            icon: "",
            id: "id",
          },
        },
      ],
      pagination: {
        pageIndex: 1,
        pagesCount: 10,
        pageSize: 10,
        totalRows: 100,
      },
    };
    yield put(paymentAccountActions.fetchPaymentAccountsSuccess(res));
  } catch (error) {
    console.error("error", error);
  }
}

export default function* paymentAccountSaga() {
  yield takeLatest(
    paymentAccountActions.fetchPaymentAccounts,
    fetchPaymentAccounts
  );
}

import { PayloadAction } from "@reduxjs/toolkit";
import { put, takeLatest } from "redux-saga/effects";
import { IBasePaging, IFilterBodyRequest } from "src/models/Bases";
import { IPaymentAccountTypeModel } from "src/models/PaymentAccountType";
import { paymentAccountTypeActions } from "./paymentAccountTypeSlice";

function* fetchPaymentAccountTypes(action: PayloadAction<IFilterBodyRequest>) {
  try {
    // const res: IBasePaging<IPaymentAccountTypeModel> = yield call(
    //   paymentAccountTypeApi.getAll,
    //   action.payload
    // );
    const res: IBasePaging<IPaymentAccountTypeModel> = {
      data: [
        {
          id: "id",
          name: "name",
          code: "code",
        },
        {
          id: "id2",
          name: "name2",
          code: "code2",
        },
      ],
      pagination: {
        pageIndex: 1,
        pagesCount: 10,
        pageSize: 10,
        totalRows: 100,
      },
    };
    yield put(paymentAccountTypeActions.fetchPaymentAccountTypesSuccess(res));
  } catch (error) {
    console.error("error", error);
  }
}

export default function* paymentAccountTypeSaga() {
  yield takeLatest(
    paymentAccountTypeActions.fetchPaymentAccountTypes,
    fetchPaymentAccountTypes
  );
}

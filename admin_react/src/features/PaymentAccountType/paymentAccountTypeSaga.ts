import { PayloadAction } from "@reduxjs/toolkit";
import { toast } from "react-toastify";
import { call, put, takeLatest } from "redux-saga/effects";
import { IBasePaging, IFilterBodyRequest } from "src/models/Bases";
import { IPaymentAccountTypeModel } from "src/models/PaymentAccountType";
import { IPaymentAccountTypeCreateOrUpdateModel } from "src/models/PaymentAccountType/Requests/IPaymentAccountTypeCreateOrUpdateModel";
import {
  default as paymentAccountApi,
  default as paymentAccountTypeApi,
} from "../../apis/paymentAccountTypeApi";
import { paymentAccountTypeActions } from "./paymentAccountTypeSlice";
const mockIPaymentAccountTypeModel: IBasePaging<IPaymentAccountTypeModel> = {
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

function* fetchPaymentAccountTypes(action: PayloadAction<IFilterBodyRequest>) {
  try {
    const res: IBasePaging<IPaymentAccountTypeModel> = yield call(
      paymentAccountTypeApi.getAll,
      action.payload
    );
    // const res = mockIPaymentAccountTypeModel;
    yield put(paymentAccountTypeActions.fetchPaymentAccountTypesSuccess(res));
  } catch (error) {
    console.error("error", error);
  }
}
function* savePaymentAccountType(
  action: PayloadAction<IPaymentAccountTypeCreateOrUpdateModel>
) {
  try {
    if (action.payload.id == null) {
      yield call(
        paymentAccountTypeApi.create,
        action.payload as IPaymentAccountTypeCreateOrUpdateModel
      );
    } else {
      yield call(
        paymentAccountTypeApi.update,
        action.payload as IPaymentAccountTypeCreateOrUpdateModel
      );
    }
    yield put(paymentAccountTypeActions.savePaymentAccountTypeSuccess({}));
    yield put(paymentAccountTypeActions.fetchPaymentAccountTypes({}));
    toast.success(`Save PaymentAccountError Successful!`);
    // history.go(-1);
  } catch (error) {
    yield put(paymentAccountTypeActions.savePaymentAccountTypeError(error));
    toast.error(`Save PaymentAccountError Error!`);
  }
}
function* fetchPaymentAccountType(action: PayloadAction<string>) {
  try {
    // call api get by id
    // const res: IPaymentAccountTypeCreateOrUpdateModel =
    //   mockIPaymentAccountTypeModel.data.find((e) => e.id == action.payload);
    const res: IPaymentAccountTypeCreateOrUpdateModel = yield call(
      paymentAccountApi.getById,
      action.payload
    );
    if (res == null) {
      yield put(paymentAccountTypeActions.fetchPaymentAccountTypeError({}));
    } else {
      yield put(paymentAccountTypeActions.fetchPaymentAccountTypeSuccess(res));
    }
  } catch (error) {
    yield put(paymentAccountTypeActions.fetchPaymentAccountTypeError({}));
  }
}

function* deletePaymentAccountType(action: PayloadAction<string>) {
  try {
    console.log(`deletePaymentAccountType`);
    // call api delete payment account type
    const result = yield call(paymentAccountApi.delete, action.payload);
    yield put(paymentAccountTypeActions.deletePaymentAccountTypeSuccess({}));
  } catch (error) {
    yield put(paymentAccountTypeActions.deletePaymentAccountTypeError({}));
  }
}

export default function* paymentAccountTypeSaga() {
  yield takeLatest(
    paymentAccountTypeActions.fetchPaymentAccountTypes,
    fetchPaymentAccountTypes
  );
  yield takeLatest(
    paymentAccountTypeActions.fetchPaymentAccountType,
    fetchPaymentAccountType
  );
  yield takeLatest(
    paymentAccountTypeActions.savePaymentAccountType,
    savePaymentAccountType
  );
  yield takeLatest(
    paymentAccountTypeActions.deletePaymentAccountType,
    deletePaymentAccountType
  );
}

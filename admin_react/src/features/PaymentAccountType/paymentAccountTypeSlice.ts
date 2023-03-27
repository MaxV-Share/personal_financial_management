import { createSelector, createSlice, PayloadAction } from "@reduxjs/toolkit";
import { get, set } from "lodash";
import { RootState } from "src/app/store";
import {
  IBaseLoading,
  IBasePaging,
  IFilterBodyRequest,
  IPagination,
} from "src/models/Bases";
import { IKeyValue } from "src/models/Common";
import { IStatus } from "src/models/Common/IStatus";
import { IPaymentAccountTypeModel } from "src/models/PaymentAccountType";
import { IPaymentAccountTypeCreateOrUpdateModel } from "../../models/PaymentAccountType/Requests/IPaymentAccountTypeCreateOrUpdateModel";

export interface IPaymentAccountTypeTable {
  data: IPaymentAccountTypeModel[];
  pagination: IPagination;
}
export interface IPaymentAccountTypeCreateOrUpdate {
  status: IStatus;
  data?: IPaymentAccountTypeCreateOrUpdateModel;
}

export interface PaymentAccountTypeState {
  status: IStatus;
  table: IBaseLoading & IPaymentAccountTypeTable;
  filterPaymentAccountTypeRequest: IFilterBodyRequest;
  langFilterRequest: IFilterBodyRequest;
  paymentAccountTypeCreateOrUpdate: IPaymentAccountTypeCreateOrUpdate;
}
const initialPagination: IPagination = {
  pageIndex: 1,
  pageSize: 5,
  pagesCount: 0,
  totalRows: 0,
};
const initialState: PaymentAccountTypeState = {
  status: IStatus.None,
  table: {
    status: IStatus.None,
    data: [],
    pagination: initialPagination,
  },
  filterPaymentAccountTypeRequest: {
    langId: "EN",
    pagination: initialPagination,
  },
  langFilterRequest: {
    langId: "",
    pagination: initialPagination,
  },
  paymentAccountTypeCreateOrUpdate: {
    // data: {},
    status: IStatus.None,
  },
};

const paymentAccountTypeSlice = createSlice({
  name: "paymentAccountType",
  initialState,
  reducers: {
    fetchPaymentAccountTypes(state, action: PayloadAction<IFilterBodyRequest>) {
      state.status = IStatus.Pending;
    },
    fetchPaymentAccountTypesSuccess(
      state,
      action: PayloadAction<IBasePaging<IPaymentAccountTypeModel>>
    ) {
      state.table.data = action.payload.data;
      state.table.pagination = action.payload.pagination;
      state.table.status = IStatus.Success;
    },
    fetchPaymentAccountType(state, action: PayloadAction<string>) {
      state.paymentAccountTypeCreateOrUpdate.status = IStatus.Pending;
    },
    fetchPaymentAccountTypeSuccess(
      state,
      action: PayloadAction<IPaymentAccountTypeCreateOrUpdateModel>
    ) {
      state.paymentAccountTypeCreateOrUpdate.status = IStatus.None;
      state.paymentAccountTypeCreateOrUpdate.data = action.payload;
    },
    fetchPaymentAccountTypeError(state, action: PayloadAction<any>) {
      state.paymentAccountTypeCreateOrUpdate.status = IStatus.Error;
      state.paymentAccountTypeCreateOrUpdate.data = null;
    },
    savePaymentAccountType(
      state,
      action: PayloadAction<IPaymentAccountTypeCreateOrUpdateModel>
    ) {
      state.paymentAccountTypeCreateOrUpdate.status = IStatus.Pending;
    },
    savePaymentAccountTypeSuccess(state, action: PayloadAction<any>) {
      console.log("savePaymentAccountTypeSuccess");
      state.paymentAccountTypeCreateOrUpdate.status = IStatus.Success;
    },
    savePaymentAccountTypeError(state, action: PayloadAction<any>) {
      state.paymentAccountTypeCreateOrUpdate.status = IStatus.Error;
    },
    resetPaymentAccountTypeStatus(state) {
      state.paymentAccountTypeCreateOrUpdate.status = IStatus.None;
    },
    resetFilter(state) {
      state.filterPaymentAccountTypeRequest = {
        langId: "EN",
        pagination: initialPagination,
      };
    },
    setFilter(state, action: PayloadAction<IKeyValue>) {
      if (
        get(state.filterPaymentAccountTypeRequest, action.payload.key) !=
        action.payload.value
      ) {
        set(
          state.filterPaymentAccountTypeRequest,
          action.payload.key,
          action.payload.value
        );
        state.table.status = IStatus.Pending;
      }
    },
    deletePaymentAccountType(state, action: PayloadAction<string>) {
      state.table.status = IStatus.Pending;
    },
    deletePaymentAccountTypeSuccess(state, action: PayloadAction<any>) {
      state.table.status = IStatus.None;
    },
    deletePaymentAccountTypeError(state, action: PayloadAction<any>) {
      state.table.status = IStatus.Error;
    },
  },
});
// Actions
export const paymentAccountTypeActions = paymentAccountTypeSlice.actions;

// Reducer
const paymentAccountTypeReducer = paymentAccountTypeSlice.reducer;
export default paymentAccountTypeReducer;

// Selectors
export const selectPaymentAccountTypeTable = createSelector(
  [
    (state: RootState) => state.paymentAccountType.table,
    (state: RootState) => state.paymentAccountType.table.data,
    (state: RootState) => state.paymentAccountType.table.status,
    (state: RootState) => state.paymentAccountType.table.pagination,
  ],
  (table) => {
    return table;
  }
);
export const selectPaymentAccountTypeStatus = (state: RootState) =>
  state.paymentAccountType.status;
export const selectPaymentAccountTypeTablePagination = (state: RootState) =>
  state.paymentAccountType.table.pagination;
export const selectFilterPaymentAccountTypeRequest = (state: RootState) =>
  state.paymentAccountType.filterPaymentAccountTypeRequest;
export const selectPaymentAccountTypeCreateOrUpdateStatus = (
  state: RootState
) => state.paymentAccountType.paymentAccountTypeCreateOrUpdate.status;
export const selectPaymentAccountTypeCreateOrUpdateData = createSelector(
  (state: RootState) =>
    state.paymentAccountType.paymentAccountTypeCreateOrUpdate.data,
  (data) => {
    console.log("selectPaymentAccountTypeCreateOrUpdateData", data);
    return data;
  }
);

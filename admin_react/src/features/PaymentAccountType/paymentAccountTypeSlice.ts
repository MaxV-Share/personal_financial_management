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
import { IPaymentAccountTypeModel } from "src/models/PaymentAccountType";

export interface IPaymentAccountTypeTable {
  data: IPaymentAccountTypeModel[];
  pagination: IPagination;
}

export interface PaymentAccountTypeState {
  isLoading: boolean;
  table: IBaseLoading & IPaymentAccountTypeTable;
  filterPaymentAccountTypeRequest: IFilterBodyRequest;
  langFilterRequest: IFilterBodyRequest;
}
const initialPagination: IPagination = {
  pageIndex: 1,
  pageSize: 5,
  pagesCount: 0,
  totalRows: 0,
};
const initialState: PaymentAccountTypeState = {
  isLoading: false,
  table: {
    isLoading: false,
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
};

const paymentAccountTypeSlice = createSlice({
  name: "paymentAccountType",
  initialState,
  reducers: {
    fetchPaymentAccountTypes(state, action: PayloadAction<IFilterBodyRequest>) {
      state.isLoading = true;
    },
    fetchPaymentAccountTypesSuccess(
      state,
      action: PayloadAction<IBasePaging<IPaymentAccountTypeModel>>
    ) {
      state.table.data = action.payload.data;
      state.table.pagination = action.payload.pagination;
      state.table.isLoading = false;
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
        state.table.isLoading = true;
      }
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
    (state: RootState) => state.paymentAccountType.table.isLoading,
    (state: RootState) => state.paymentAccountType.table.pagination,
  ],
  (table) => {
    return table;
  }
);
export const selectPaymentAccountTypeTableLoading = (state: RootState) =>
  state.paymentAccountType.isLoading;
export const selectPaymentAccountTypeTablePagination = (state: RootState) =>
  state.paymentAccountType.table.pagination;
export const selectFilterPaymentAccountTypeRequest = (state: RootState) =>
  state.paymentAccountType.filterPaymentAccountTypeRequest;

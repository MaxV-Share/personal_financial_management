import { createSelector, createSlice, PayloadAction } from "@reduxjs/toolkit";
import { RootState } from "src/app/store";
import {
  IBaseLoading,
  IFilterBodyRequest,
  IPagination,
} from "src/models/Bases";
import { ITransactionPerDateModelList } from "src/models/Transaction/ITransactionPerDateModelList";

export interface TransactionState {
  isLoading: boolean;
  transactionPerDateList: IBaseLoading & ITransactionPerDateModelList;
  filterTransactionRequest: IFilterBodyRequest;
  langFilterRequest: IFilterBodyRequest;
}
const initialPagination: IPagination = {
  pageIndex: 1,
  pageSize: 5,
  pagesCount: 0,
  totalRows: 0,
};
const initialState: TransactionState = {
  isLoading: false,
  transactionPerDateList: {
    isLoading: false,
    data: [],
    pagination: initialPagination,
  },
  filterTransactionRequest: {
    langId: "EN",
    pagination: initialPagination,
  },
  langFilterRequest: {
    langId: "",
    pagination: initialPagination,
  },
};

const transactionSlice = createSlice({
  name: "transaction",
  initialState,
  reducers: {
    fetchTransactions(state, action: PayloadAction<IFilterBodyRequest>) {
      state.isLoading = true;
      state.transactionPerDateList.isLoading = true;
    },
    fetchTransactionsSuccess(
      state,
      action: PayloadAction<ITransactionPerDateModelList>
    ) {
      state.isLoading = true;
      state.transactionPerDateList.isLoading = false;
      state.transactionPerDateList.data = action.payload.data;
      state.transactionPerDateList.pagination = action.payload.pagination;
    },
  },
});
// Actions
export const transactionActions = transactionSlice.actions;

// Reducer
const transactionReducer = transactionSlice.reducer;
export default transactionReducer;

// Selectors
export const selectTransactionList = createSelector(
  [
    (state: RootState) => state.transaction.transactionPerDateList,
    (state: RootState) => state.transaction.transactionPerDateList.data,
    (state: RootState) => state.transaction.transactionPerDateList.isLoading,
    (state: RootState) => state.transaction.transactionPerDateList.pagination,
  ],
  (transactionPerDateList) => {
    return transactionPerDateList;
  }
);
export const selectTransactionsLoading = (state: RootState) =>
  state.transaction.isLoading;
export const selectTransactionsPagination = (state: RootState) =>
  state.transaction.transactionPerDateList.pagination;
export const selectFilterTransactionRequest = (state: RootState) =>
  state.transaction.filterTransactionRequest;

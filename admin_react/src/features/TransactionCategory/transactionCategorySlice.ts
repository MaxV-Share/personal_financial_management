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
import { ITransactionCategoryModel } from "src/models/TransactionCategory/ITransactionCategoryModel";
import { ITransactionCategoryTable } from "src/models/TransactionCategory/ITransactionCategoryTable";

export interface TransactionCategoryState {
  status: IStatus;
  table: IBaseLoading & ITransactionCategoryTable;
  filterTransactionCategoryRequest: IFilterBodyRequest;
  langFilterRequest: IFilterBodyRequest;
}
const initialPagination: IPagination = {
  pageIndex: 1,
  pageSize: 5,
  pagesCount: 0,
  totalRows: 0,
};
const initialState: TransactionCategoryState = {
  status: IStatus.None,
  table: {
    status: IStatus.None,
    data: [],
    pagination: initialPagination,
  },
  filterTransactionCategoryRequest: {
    langId: "EN",
    pagination: initialPagination,
  },
  langFilterRequest: {
    langId: "",
    pagination: initialPagination,
  },
};

const transactionCategorySlice = createSlice({
  name: "transactionCategory",
  initialState,
  reducers: {
    fetchTransactionCategories(
      state,
      action: PayloadAction<IFilterBodyRequest>
    ) {
      state.status = IStatus.Pending;
    },
    fetchTransactionCategoriesSuccess(
      state,
      action: PayloadAction<IBasePaging<ITransactionCategoryModel>>
    ) {
      state.table.data = action.payload.data;
      state.table.pagination = action.payload.pagination;
      state.table.status = IStatus.Success;
    },
    resetFilter(state) {
      state.filterTransactionCategoryRequest = {
        langId: "EN",
        pagination: initialPagination,
      };
    },
    setFilter(state, action: PayloadAction<IKeyValue>) {
      if (
        get(state.filterTransactionCategoryRequest, action.payload.key) !=
        action.payload.value
      ) {
        set(
          state.filterTransactionCategoryRequest,
          action.payload.key,
          action.payload.value
        );
        state.table.status = IStatus.Pending;
      }
    },
  },
});
// Actions
export const transactionCategoryActions = transactionCategorySlice.actions;

// Reducer
const transactionCategoryReducer = transactionCategorySlice.reducer;
export default transactionCategoryReducer;

// Selectors
export const selectTransactionCategoryTable = createSelector(
  [
    (state: RootState) => state.transactionCategory.table,
    (state: RootState) => state.transactionCategory.table.data,
    (state: RootState) => state.transactionCategory.table.status,
    (state: RootState) => state.transactionCategory.table.pagination,
  ],
  (table) => {
    return table;
  }
);
export const selectTransactionCategoryTableLoading = (state: RootState) =>
  state.transactionCategory.status;
export const selectTransactionCategoryTablePagination = (state: RootState) =>
  state.transactionCategory.table.pagination;
export const selectFilterTransactionCategoryRequest = (state: RootState) =>
  state.transactionCategory.filterTransactionCategoryRequest;

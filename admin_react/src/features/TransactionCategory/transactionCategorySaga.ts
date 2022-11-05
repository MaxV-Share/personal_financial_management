import { PayloadAction } from "@reduxjs/toolkit";
import { put, takeLatest } from "redux-saga/effects";
import { IBasePaging, IFilterBodyRequest } from "src/models/Bases";
import { ITransactionCategoryModel } from "src/models/TransactionCategory/ITransactionCategoryModel";
import { transactionCategoryActions } from "./transactionCategorySlice";

function* fetchTransactionCategories(
  action: PayloadAction<IFilterBodyRequest>
) {
  try {
    const res: IBasePaging<ITransactionCategoryModel> = {
      data: [
        {
          id: "id1",
          name: "name1",
          description: "description1",
          icon: "symbol1",
          code: "code1",
          // parentId: "parentId",
          // parentName: "parentName",
          // parentDescription: "parentDescription",
          childrens: [
            {
              id: "id1.1",
              name: "name 1.1",
              description: "description 1.1",
              icon: "symbol 1.1",
              code: "code 1.1",
              parentId: "id1",
              parentName: "name1",
              parentDescription: "description1",
            },
          ],
        },
        {
          id: "id2",
          name: "name2",
          description: "description2",
          icon: "symbol2",
          code: "code2",
          // parentId: "parentId",
          // parentName: "parentName",
          // parentDescription: "parentDescription",
          childrens: [
            {
              id: "id2.1",
              name: "name21",
              description: "description21",
              icon: "symbol21",
              code: "code21",
              parentId: "id2",
              parentName: "name2",
              parentDescription: "description2",
            },
            {
              id: "id2.2",
              name: "name 2.2",
              description: "description 2.2",
              icon: "symbol 2.2",
              code: "code 2.2",
              parentId: "id2",
              parentName: "name2",
              parentDescription: "description2",
            },
          ],
        },
      ],
      pagination: {
        pageIndex: 1,
        pagesCount: 10,
        pageSize: 10,
        totalRows: 100,
      },
    };
    yield put(
      transactionCategoryActions.fetchTransactionCategoriesSuccess(res)
    );
  } catch (error) {
    console.error("error", error);
  }
}

export default function* transactionCategorySaga() {
  yield takeLatest(
    transactionCategoryActions.fetchTransactionCategories,
    fetchTransactionCategories
  );
}

import { IBaseTransactionCategoryModel } from "./IBaseTransactionCategoryModel.";

export interface ITransactionCategoryModel
  extends IBaseTransactionCategoryModel {
  parentId?: string;
  parentName?: string;
  parentDescription?: string;
  childrens?: ITransactionCategoryModel[];
}

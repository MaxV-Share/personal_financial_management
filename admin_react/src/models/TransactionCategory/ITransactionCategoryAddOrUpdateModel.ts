import { IBaseTransactionCategoryModel } from "./IBaseTransactionCategoryModel.";

export interface ITransactionCategoryAddOrUpdateModel
  extends IBaseTransactionCategoryModel {
  name?: string;
  description?: string;
  icon?: string;
  parentId?: string;
  parentName?: string;
  parentDescription?: string;
  type?: string;
  typeId?: string;
}

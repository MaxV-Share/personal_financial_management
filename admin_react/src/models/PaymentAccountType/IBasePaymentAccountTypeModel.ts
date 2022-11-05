import { IBaseModel } from "../Bases";
export interface IBasePaymentAccountTypeModel extends IBaseModel<string> {
  /**
   * The payment account type code identifier
   */
  code: string;

  /**
   * The payment account type name
   */
  name: string;
}

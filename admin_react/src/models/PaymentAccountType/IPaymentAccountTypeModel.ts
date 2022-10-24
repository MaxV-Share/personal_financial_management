import { IBaseModel } from "../Bases";

export interface IPaymentAccountTypeModel extends IBaseModel<string> {
  /**
   * The payment account code identifier
   */
  code: string;

  /**
   * The payment account name
   */
  name: string;
}

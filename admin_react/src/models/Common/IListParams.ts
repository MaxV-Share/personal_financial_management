import { IMaxOrder } from "./IMaxOrder";


export interface IListParams {
  _page?: number;
  _limit?: number;
  _sort?: string;
  _order?: IMaxOrder;

  [key: string]: any;
}
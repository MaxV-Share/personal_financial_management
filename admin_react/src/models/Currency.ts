import { BaseModel } from "./BaseModels/Base";

export class Currency extends BaseModel<string> {
  code?: string;
  Name?: string;
  Icon?: string;
  Description?: string;
}

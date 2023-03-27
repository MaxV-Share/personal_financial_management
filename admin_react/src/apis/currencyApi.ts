import { IBasePaging } from "src/models/Bases";
import { ICurrencyModel } from "src/models/Currency";
import { IFilterBodyRequest } from "../models/Bases/IFilterBodyRequest";
import axiosClient from "./axiosClient";

const currencyApi = {
  getAll(data: IFilterBodyRequest): Promise<IBasePaging<ICurrencyModel>> {
    const url = "currencies/filter";
    return axiosClient().post(url, data);
  },
};

export default currencyApi;

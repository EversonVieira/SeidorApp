import { BaseResponse } from "./base-response";

export class ListResponse<T> extends BaseResponse<T[]>  {
  totalItemsOnPage: number = 0;
  pageIndex: number = 0;
  totalItems: number = 0;
}

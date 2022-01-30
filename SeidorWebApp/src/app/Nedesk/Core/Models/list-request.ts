import { BaseRequest } from "./base-request";
import { Filter } from "./filter";

export class ListRequest{
  limit: number = 0;
  pageIndex: number = 0;
  filters:Filter[] = [];
}

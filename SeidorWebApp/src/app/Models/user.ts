import { BaseModel } from "../Nedesk/Core/Models/base-model";

export class User extends BaseModel {
    name:string = "";
    email:string = "";
    password:string = "";
}



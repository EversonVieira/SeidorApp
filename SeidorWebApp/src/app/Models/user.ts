import { BaseModel } from "../Nedesk/Core/Models/base-model";

export class User extends BaseModel {
    name:string = "";
    email:string = "";
    password:string = "";
}

export class DTO_RegisterUser extends User{
    confirmPassword:string = "";
}


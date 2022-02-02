import { User } from "./user";


export class DTO_RegisterUser extends User {
    confirmPassword: string = "";
}

export class DTO_UpdateUser extends DTO_RegisterUser {
    oldPassword:string = '';
}

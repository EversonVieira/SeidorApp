import { User } from "src/app/Models/user";

export class CurrentUser {

    private static user:User = new User();

    
    static getUser():User{
        return this.user;
    }

    static setUser(user:User){
        Object.assign(this.user,user);
    }
}

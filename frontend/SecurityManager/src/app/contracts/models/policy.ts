import { IEntity } from "./IEntity";

export class Policy implements IEntity {
    get IdNumber(): number{
        return this.IdPolicy;
    }
    IdPolicy: number;
    Name: string;

    asSecObject(): SecObject{
        var secObject = new SecObject();
        secObject.IdSecObject = this.IdPolicy;
        secObject.ObjectName = this.Name;

        return secObject;
    }

    static readonly ShowUsers = "showUsers";
    static readonly ShowRoles = "showRoles";
    static readonly ShowGroups = "showGroups";
    static readonly AddNewUser = "addNewUser";
    static readonly RemoveUser = "removeUser";
    static readonly EditUserProfile = "editUserProfile";
    static readonly EditUserGroups = "editUserGroups";
    static readonly EditUserRoles = "editUserRoles";
    
}

export class SecObject{
    IdSecObject: number;
    ObjectName: string;
}
import { Member } from "./member";
import { IEntity } from "./IEntity";

export class User implements IEntity{
    get IdNumber(): number{
        return this.IdMember;
    }
    IdMember: number;
    get Login(): string{
        return this.Name;
    }

    set Login(value: string){
        this.Name = value;
    }
    Name: string;
    FirstName: string;
    LastName: string;
    MiddleName: string;
    Email: string;
    Status: boolean;
    DateCreated: Date;
    LastActivityDate: Date;
    public get StatusName() : string {
        let statusName = this.Status ? "Active" : "Inactive";
        console.log(`statusName=${statusName}`);
        return statusName;
    }
    
    public set StatusName(v : string) {
        
    }
    
}

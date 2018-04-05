import { Member } from "./member";

export class User extends Member{
    Login: string;
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

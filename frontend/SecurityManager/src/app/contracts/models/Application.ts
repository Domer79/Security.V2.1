import { IEntity } from "./IEntity";

export class Application implements IEntity{
    
    public get Name() : string {
        return this.AppName;
    }

    public set Name(value: string){
        this.AppName = value;
    }
    
    get IdNumber(): number{
        return this.IdApplication;
    }
    
    IdApplication: number;
    AppName: string;
    Description: string;
}
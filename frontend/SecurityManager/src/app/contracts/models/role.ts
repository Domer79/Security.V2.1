import { IEntity } from "./IEntity";

export class Role implements IEntity {
    get IdNumber(): number{
        return this.IdRole;
    }
    IdRole: number;
    Name: string;
    Description: string;
}

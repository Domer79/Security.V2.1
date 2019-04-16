import { IEntity } from "./IEntity";

export class Member implements IEntity {

    get IdNumber(): number{
        return this.IdMember;
    }

    Id: string;
    IdMember: number;
    Name: string;
}

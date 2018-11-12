import { IEntity } from "./IEntity";

export class Policy implements IEntity {
    IdPolicy: number;
    Name: string;

    asSecObject(): SecObject{
        var secObject = new SecObject();
        secObject.IdSecObject = this.IdPolicy;
        secObject.ObjectName = this.Name;

        return secObject;
    }
}

export class SecObject{
    IdSecObject: number;
    ObjectName: string;
}
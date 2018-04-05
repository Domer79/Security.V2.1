export class Policy {
    IdPolicy: number;
    Name: string;

    asSecObject(): SecObject{
        var secObject = new SecObject();
        secObject.IdSecObject = this.IdPolicy;
        secObject.ObjectName = this.Name;

        return secObject;
    }
}

class SecObject{
    IdSecObject: number;
    ObjectName: string;
}
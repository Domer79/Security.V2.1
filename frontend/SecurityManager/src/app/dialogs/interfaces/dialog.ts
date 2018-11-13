import { IEntity } from "../../contracts/models/IEntity";

export interface Dialog{
    open(): void;
    content;
}

export interface SelectDialog extends Dialog{
    select(item: IEntity): void;
}
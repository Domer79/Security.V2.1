import { Application } from "./models/Application";
import { Observable } from "rxjs/Observable";

export interface ApplicationContext{
    Application: Observable<Application>;
}
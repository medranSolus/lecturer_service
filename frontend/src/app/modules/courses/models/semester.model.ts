import { deserialize } from "cerialize";

export class Semester {

    @deserialize
    id: string;

    @deserialize
    isWinter: boolean;

    @deserialize
    startYear: number;

    @deserialize
    startMonth: number;

    @deserialize
    startDay: number;

    @deserialize
    endYear: number;

    @deserialize
    endMonth: number;

    @deserialize
    endDay: number;
    
}
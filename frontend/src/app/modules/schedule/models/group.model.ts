import { deserialize } from "cerialize"

export class Group {
    @deserialize
    id: string
    
    @deserialize
    courseID: string
    
    @deserialize
    studentsCount: number
    
    @deserialize
    room: string

    @deserialize
    building: string

    @deserialize
    weekTypeID: number

    @deserialize
    dayID: number

    @deserialize
    startHour: number
    
    @deserialize
    startMinute: number

    @deserialize
    endHour: number

    @deserialize
    endMinute: number

    @deserialize
    lecturerID: string
}
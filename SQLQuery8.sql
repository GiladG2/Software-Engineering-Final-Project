-- Drop the existing constraints
-- Recreate the foreign key constraints with ON DELETE CASCADE and ON UPDATE CASCADE

ALTER TABLE dbo.tblExercise_List
ADD CONSTRAINT FK_Exercise_MuscleGroup FOREIGN KEY (fldMuscleGroup)
    REFERENCES dbo.tblMuscleGroup (fldMuscle_Group_Id)
    ON DELETE CASCADE
    ON UPDATE CASCADE;

ALTER TABLE dbo.tblExercise_List
ADD CONSTRAINT FK_Exercise_TimeToComplete FOREIGN KEY (fldTime_To_Complete)
    REFERENCES dbo.tblTimeToComplete (fldTimeToComplete_Id)
    ON DELETE CASCADE
    ON UPDATE CASCADE;

ALTER TABLE dbo.tblMuscles_Worked_In_Exercises
ADD CONSTRAINT FK_tblMuscles_Worked_In_Exercises_fldExercise_Id FOREIGN KEY (fldExercise_Id)
    REFERENCES dbo.tblExercise_List (fldExercise_Id)
    ON DELETE CASCADE
    ON UPDATE CASCADE;

ALTER TABLE dbo.tblMuscles_Worked_In_Exercises
ADD CONSTRAINT FK_tblMuscles_Worked_In_Exercises_fldMuscle_Id FOREIGN KEY (fldMuscle_Id)
    REFERENCES dbo.tblMuscles_List (fldMuscle_Id)
    ON DELETE CASCADE
    ON UPDATE CASCADE;

ALTER TABLE dbo.tblAnalytics
ADD CONSTRAINT FK_tblAnalytics_User FOREIGN KEY (fldUser_Id)
    REFERENCES dbo.tblUsers (fldUser_Id)
    ON DELETE CASCADE
    ON UPDATE CASCADE;

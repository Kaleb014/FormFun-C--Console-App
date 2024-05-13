using System;

namespace FormFun
{
    static class State
    {

        public static string state = "program_start";
        public static string lastState = "";
        private static string currentFocus = "form grid";

        public static void SwitchState()
        {
            switch (currentFocus)
            {
                case "form grid":
                    switch (state)
                    {
                        case "free":
                            Console.WriteLine("Type a command and press the enter key.\n");
                            break;

                        case "new form":
                            string _newDescription;

                            while (state == "new form")
                            {
                                Console.Write("New Form Description: ");
                                _newDescription = Console.ReadLine();

                                if (_newDescription == "\n") _newDescription = "";

                                if (!HelperMethods.IsStringInvalid(_newDescription))
                                {
                                    if (DataManager.forms.ContainsKey(_newDescription))
                                    {
                                        Console.WriteLine($"A Form with the description of \"{_newDescription}\" already exists!");
                                    }
                                    else
                                    {
                                        Form newForm = new Form(_newDescription);
                                        DataManager.forms.Add(_newDescription, newForm);
                                        Console.WriteLine($"Created Form: {newForm.description}");
                                        DataManager.SaveToBinaryFile(DataManager.savePath, DataManager.forms, false);
                                        state = "free";
                                    }
                                }
                            }

                            break;

                        case "edit form":
                            while (state == "edit form")
                            {
                                foreach (Form form in DataManager.forms.Values)
                                {
                                    Console.WriteLine(form.description);
                                }

                                Console.Write("What form do you want to edit? ");
                                string _formDescription = Console.ReadLine();
                                if (!HelperMethods.IsStringInvalid(_formDescription) && DataManager.forms.ContainsKey(_formDescription))
                                {
                                    DataManager.currentForm = DataManager.forms[_formDescription];
                                    currentFocus = "form";
                                    lastState = "free";
                                    state = "commands";
                                }
                            }
                            break;

                        case "copy form":
                            while (state == "copy form")
                            {
                                foreach (Form form in DataManager.forms.Values)
                                {
                                    Console.WriteLine(form.description);
                                }

                                Console.Write("What form do you want to copy? ");
                                string _formDescription = Console.ReadLine();
                                while (state != "commands")
                                {
                                    if (!HelperMethods.IsStringInvalid(_formDescription) && DataManager.forms.ContainsKey(_formDescription))
                                    {
                                        Console.Write("\nPlease provide a unique description for the copied form.\n" +
                                            "Form Description: ");
                                        _newDescription = Console.ReadLine();
                                        if (!HelperMethods.IsStringInvalid(_newDescription))
                                        {
                                            if (!DataManager.forms.ContainsKey(_newDescription))
                                            {
                                                DataManager.SaveToBinaryFile(DataManager.copyPath, DataManager.forms[_formDescription]);
                                                DataManager.currentForm = DataManager.TryLoad<Form>(DataManager.copyPath);
                                                DataManager.currentForm.description = _newDescription;
                                                DataManager.forms.Add(DataManager.currentForm.description, DataManager.currentForm);

                                                Console.WriteLine($"\nCopied {_formDescription} to {_newDescription}.\n");

                                                DataManager.SaveToBinaryFile(DataManager.savePath, DataManager.forms, false);

                                                state = "commands";
                                            }
                                            else
                                            {
                                                Console.WriteLine($"\nA Form with the description of \"{_newDescription}\" already exists!");
                                            }
                                        }
                                        else
                                        {
                                            Console.WriteLine("\nEnter a valid description.");
                                        }
                                    }
                                }
                            }
                            break;

                        case "delete form":
                            while (state == "delete form")
                            {
                                foreach (Form form in DataManager.forms.Values)
                                {
                                    Console.WriteLine(form.description);
                                }

                                Console.Write("What form do you want to delete? ");

                                string _formDescription = Console.ReadLine();

                                if (!HelperMethods.IsStringInvalid(_formDescription) && DataManager.forms.ContainsKey(_formDescription))
                                {
                                    bool _needConfirmation = true;
                                    
                                    while (_needConfirmation)
                                    {
                                        Console.Write($"Are you sure you want to delete {_formDescription}? Type Yes or No... ");
                                        string _userResponse = Console.ReadLine().ToLower();

                                        if (_userResponse == "yes")
                                        {
                                            DataManager.forms.Remove(_formDescription);
                                            Console.WriteLine($"Deleted form \"{_formDescription}\".");
                                            _needConfirmation = false;
                                            state = "free";
                                        }
                                        else if (_userResponse == "no")
                                        {
                                            _needConfirmation = false;
                                            state = "free";
                                        }
                                        else
                                        {
                                            Console.WriteLine("Invalid response!");
                                        }
                                    }
                                }
                            }
                            break;

                        case "clear":
                            Console.Clear();
                            state = "free";
                            break;

                        case "commands":
                            Console.WriteLine(
                            "+-------------------------------------------------------------------+\n" +
                            "| New Form | Edit Form | Copy Form | Delete Form | Clear | Commands |\n" +
                            "+-------------------------------------------------------------------+\n");
                            state = "free";
                            break;

                        case "program_start":
                            state = "commands";
                            SwitchState();
                            state = "free";
                            SwitchState();
                            break;

                        default:
                            Console.WriteLine("That is not a valid command.\n");
                            state = "free";
                            return;
                    }
                    break;

                case "form":
                    switch (state)
                    {
                        case "free":
                            Console.WriteLine("Type a command and press the enter key.\n");
                            break;

                        case "edit name":
                            while (state == "edit name")
                            {
                                Console.Write("Form Description: ");
                                string _newDescription = Console.ReadLine();

                                if (!HelperMethods.IsStringInvalid(_newDescription))
                                {
                                    if (DataManager.forms.ContainsKey(_newDescription))
                                    {
                                        Console.WriteLine($"A Form with the description of \"{_newDescription}\" already exists!");
                                    }
                                    else
                                    {
                                        DataManager.oldFormName = DataManager.currentForm.description;
                                        DataManager.currentForm.description = _newDescription;
                                        DataManager.formNameChanged = true;
                                        Console.WriteLine($"Renamed Form: {DataManager.currentForm.description}");
                                        state = "free";
                                    }
                                }
                            }
                            break;

                        case "add tab":
                            state = "free";
                            break;

                        case "edit tab":
                            state = "free";
                            break;

                        case "copy tab":
                            state = "free";
                            break;

                        case "delete tab":
                            state = "free";
                            break;

                        case "clear":
                            Console.Clear();
                            state = "free";
                            break;

                        case "commands":
                            Console.WriteLine(
                            "\n" +
                            "+-------------------------------------------------------------------------------------------+\n" +
                            "| Edit Name | Add Tab | Edit Tab | Copy Tab | Delete Tab | Clear | Commands | Cancel | Save |\n" +
                            "+-------------------------------------------------------------------------------------------+\n");
                            state = "free";
                            SwitchState();
                            break;

                        case "cancel":
                            DataManager.LoadBinaryFromFile(DataManager.savePath);
                            currentFocus = "form grid";
                            state = "commands";
                            break;

                        case "save":
                            if(DataManager.formNameChanged)
                            {
                                DataManager.forms.Remove(DataManager.oldFormName);
                                DataManager.forms.Add(DataManager.currentForm.description, DataManager.currentForm);
                            }

                            DataManager.SaveToBinaryFile(DataManager.savePath, DataManager.forms, false);
                            currentFocus = "form grid";
                            state = "commands";
                            break;

                        default:
                            Console.WriteLine("That is not a valid command.\n");
                            state = "free";
                            return;
                    }
                    break;

                default:
                    return;
            }
        }
    }
}

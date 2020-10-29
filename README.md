# hasso
HomeAssistant-Organizer


![.NET Core](https://github.com/earloc/hasso/workflows/.NET%20Core/badge.svg) ![NuGet](https://img.shields.io/nuget/v/hasso)

# What is it?

The **h**ome-**ass**istant-**o**rganizer, or hasso, is a small cli helping out doing some tedious work when manually modifying home-assistant's configuration-yamls.
Hasso can split up home-assistant's configuration-yamls in many smaller ones, which then can be edited and organized more easily and later packed up again into monolithic versions home-assistant comes with OOTB.

> If you already utilize [splitted configurations], this tool might be of no use for you
>
> If not, read on ;)

# How to get it?

## dotnet global tool
Run the follwoing command to download hasso as a global dotnet-tool [from nuget.org]

```
dotnet tool install --global hasso
```

# Verbs / commands

Hasso uses a verb-style cli, similar to git or other popular cli's out there.
To show some help about the possible verbs and it's parameters, just ommit any parameters:

```
Usage:
  hasso [options] [command]

Options:
  --version         Show version information
  -?, -h, --help    Show help and usage information

Commands:
  explode, fass!, split, -s       splits monolithic yamls (scenes.yaml, scripts.yaml, ...) into many smaller ones
  aus!, compose, implode, -c    composes multiple partial-yamls into monolithic ones, ready to deploy
```

## Split

Invoking ``` dotnet hasso split ``` will look in the current working directory and search for 
- ```scenes.yaml```
- ```scripts.yaml```
- ```automations.yaml```

Hasso then parses it´s contents and split them up at root level - generating ```*.partial.yamls``` into the respective sub-directories:
- ```scenes```
- ```scripts```
- ```automations```

> german folks may also try ```dotnet hasso fass!``` ;)

### Example
Given the following 
- ```scripts.yaml```:
  ```yaml
  some_script_name_1:
    alias: Some.Script.Name 1
    sequence:
    - type: turn_on
      ...
    mode: restart
  some_script_name_2:
    alias: Some.Script.Name 2
    ...
    mode: restart
  ```
```dotnet hasso split``` will generate the following files:

- ```./scripts/Some.Script.Name 1.partial.yaml```
  ```yaml
  some_script_name_1:
    alias: Some.Script.Name 1
    sequence:
    - type: turn_on
      ...
    mode: restart
  ```
- ```./scripts/Some.Script.Name 2.partial.yaml```
  ```yaml
  some_script_name_2:
    alias: Some.Script.Name 2
    ...
    mode: restart
  ```

Same applies to previous mentioned config-files.


## Compose

Invoking ``` dotnet hasso compose ``` will gather all ```*.partial.yaml```-files underneath the following sub-directories of the current working directory:
- ```scenes```
- ```scripts```
- ```automations```

and pack them up to their respective counterparts:
- ```scenes.yaml```
- ```scripts.yaml```
- ```automations.yaml```

which then can be deployed back onto your HA-instance, easily.

> remember to reload the affected configurations in HA´s UI afterwards, to see the changes take effect.

> german folks may also try ```dotnet hasso aus!``` ;)


## Debugger

Invoking ``` dotnet hasso debugger ``` will fire up a simple web-ui and web-api which can be used as a devices stub.
This can come in handy, when you´re playing around with new automations or scripts, but don´t want the actual devices to respond to the commands they get send from home-assistant.

Seting up the debugger to recieve calls instead of the actual devices currently involves some branching within scripts and automations, which I might cover in a later ost or update of this readme.

Stay tuned for updates.


# But...why?

I recently started tackling around with home-automation, where I found myself doing a lot of repetitive tasks involving
1. making edits in home-assistant's ```UI``` 
    > e.g. create scripts, automations, etc. to get accustomed with the way home-assistant is organized
2. manually copy home-assistant's configuration yamls from home-assistant-instance onto my local machine
3. commit the current version(s) into a git repo
    > to easy roll back when I happen to f**k things up in the next step
3. make some manuall edits / tweaks, etc.
    > while still hoping I don't f**k up **that** hard ;)
4. ```git commit``` even those local changes
    > in case I did ;)
5. ```copy``` all back onto my home-assistant-instance, reload the configs and hope for the best

Which - for myself - I refer to as the ```pull/modify/push```-loop.

As I started to automate more and more within my home, I quickly got bored a bit about the steps involved in this process and strived for a faster *inner-loop*, especially regarding **step 2.** from above.

Manually messsing around in the quickly growing yaml-files hurts (me), even with [this little gem here].

> I'm aware of the [splitted configurations]-feature home-assistant offers kind of OOTB - however, I'm still not there yet - so bare with me if this tool here does not make any sense. For the time beeing, it makes a huge difference in my current loop.

# Contributing
- file an issue
- fork the repository
- make the changes
- submit a pull-request
- hope for the best ;)
  

[from nuget.org]:https://www.nuget.org/packages/Hasso/
[home-assistant]:https://www.home-assistant.io/
[splitted configurations]:https://www.home-assistant.io/docs/configuration/splitting_configuration/
[this little gem here]:https://marketplace.visualstudio.com/items?itemName=keesschollaart.vscode-home-assistant
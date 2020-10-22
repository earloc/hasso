# hasso
HomeAssistant-Organizer


![.NET Core](https://github.com/earloc/hasso/workflows/.NET%20Core/badge.svg) ![NuGet](https://img.shields.io/nuget/v/hasso)

## What is it?

The Home-ASSistant-Organizer, or short hasso, is a small cli helping out doing some tideous work when manually modifying home-assistant's configuration-yamls.
Hasso can split up home-assistant's configuration-yamls in many smaller ones, which then can be edited and organized more easily and later packed up again into monolithic versions home-assistant comes with OOTB.

> If you already utilize [splitted configurations] home-assistant also offers, this tool might be of no use for you
> If not, read on ;)

## How to get it?

### dotnet global tool
Run the follwoing command to download [hasso] as a global dotnet-tool

```
dotnet tool install --global hasso
```

### Verbs

hasso uses a verb-style CLI, similar to git or other popular cli's out there.
To show some help about the possible verbs and it's parameters, just ommit any parameters

#### split


#### compose

## But...why?

I recently started tackling around with home-automation, where I found myself doing a lot of repetitive tasks involving
1. making edits in home-assistant's ```UI``` 
    > e.g. create scripts, automations, etc. to get accustomed with the way home-assistant is organized
2. manually copy home-assistant's configuration yamls from my PI onto my local machine
3. commit the current version(s) into a git repo
    > to easy roll back when I happen to f**k things up in the next step
3. make some manuall edits / tweaks, etc.
    > while still hoping I don't f**k up **that** hard ;)
4. ```git commit``` even those local changes
    > in case I did ;)
5. ```copy``` all back onto my home-assistant-instance, reload the configs and hope for the best

Which - for myself - I refer to as the ```pull/modify/push```-loop.

As I started to automate more and more within my home, I quickly got bored a bit about the above steps involved in this process and strived for a faster *inner-loop*, especially regarding **step 2.** from above.

Manually messsing around in the quickly growing yaml-files hurts (me), even with [this little gem here].

> I'm aware of the [splitted configurations]-feature home-assistant offers kind of OOTB - however, I'm still not there yet - so bare with me if this tool here does not make any sense. For me, it makes a huge difference, at least atm.


## Contributions welcome:
- file an issue
- fork the repository
- make the changes
- submit a pull-request
- hope for the best ;)
  

[PI]:()
[home-assistant]:(https://www.home-assistant.io/)
[advanced scenarios offered by home-assistant itself]:(https://www.home-assistant.io/docs/configuration/splitting_configuration/)
[splitted configurations]:(https://www.home-assistant.io/docs/configuration/splitting_configuration/)
[this little gem here]:(https://marketplace.visualstudio.com/items?itemName=keesschollaart.vscode-home-assistant)
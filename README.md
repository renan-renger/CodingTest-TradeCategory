# CodingTest-TradeCategory
Coding Test - 09/2022

## Question 2 
A  new  category  was  created  called  PEP  (politically  exposed  person).  
Also,  a  new  bool  property IsPoliticallyExposed was created in the ITrade interface.   
A trade shall be categorized as PEP if IsPoliticallyExposedis true. 
Describe in at most 1 paragraph what you must do in your design to account for this new category.

## Answer
Create new property in ITrade (Application) and Trade (Domain). Include PEP category in TradeCategoryEnum (Application).
Include new method in TradeService (Infrastructure) for validation. Method returns null is not-PEP and TradeCategoryEnum.PEP if PEP.
Call said method in TradeService.ValidateTrade, including it in the correct hierarch, using null-coalescing statement.
  - If "stronger" than EXPIRED, include before IsTradeExpired.
  - If "stronger" than HIGHRISK or MEDIUMRISK but "weaker" than EXPIRED, include between IsTradeExpired and RiskAssessment.
  - If "weaker" than all others, include after RiskAssessment.

Null-coalescing will ensure that, if a "stronger" category is trigger, "weaker" categories will not be evaluted.

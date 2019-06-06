# EquivalenceIndependence
This package provides two tests to show equivalence of the two way contingency tables to the independence model.
The package is based on the article:

Vladimir Ostrovski, “Testing equivalence to families of multinomial distributions with application to the independence model
”, 
Statistics and Probability Letters 139 (2018), 61–66.

The package is written in VB.NET. Tree examples are available in the module "StartMod.vb", which can be run immediately.

The goal is to show that two categorial random variables are approximately independent distributed. 
The approximate independence can be important for some applications or also greatly simplify calculations. 
However, the testing of the equivalence is a difficult task.

Let d denote Euclidean distance and let M be the independence model, 
which contains all product measures of the corresponding dimensions. 
Two categorial random variables are considered approximately independent for the tolerance parameter e>0, 
if the joint probability density p fulfills the following condition: 
There exists a product measure q in M such that d(p,q)<e. 
The minimum distance between p and the model M is defined as the minimum  of d(p,q), where q ranges over M.
 
The equivalence test problem is formally stated by 
H0={d(p,M) >=e } against H1={d(p,M)<e}, where e>0 is the tolerance parameter.
 
The goal is to reject the hypothesis of the non-equivalency H0   at a significance level alpha.

The package provides two tests for that purpose: the asymptotic test and the bootstrap test. 
Both tests are available as functions in the module “tests two way collapsibility”, 
which return the class “TestResult” as result. The class “TestResult” contains two public fields only:

• Field “result” is Boolean. The value is true if the test rejects H0 and false otherwise.
• Field “minEps” is double. This is the smallest tolerance parameter, for which the test can reject H0.

The asymptotic test is based on the asymptotic distribution of the test statistic. 
Therefore the asymptotic test need some sufficiently large number of the observations. 
It should be used carefully because the test is approximate and may be anti conservative at some points. 
In order to obtain a conservative test reducing of alpha  (usually halving) or 
slight shrinkage of the tolerance parameter e may be appropriate. 
The asymptotic test is realized as the function “asymptoticTest”, which has the following parameters:

• p is a two dimensional array of double. It should contain two way contingency table.
• n is the number of observations.
• alpha is the significance level.
• epsilon is the tolerance parameter e.

The bootstrap test is based on the re-sampling method called bootstrap. 
The bootstrap test is more precise and reliable than the asymptotic test. 
However, it should be used carefully because the test is approximate and may be anti conservative at some points. 
In order to obtain a conservative test reducing of alpha  (usually halving) or 
slight shrinkage of the tolerance parameter e may be appropriate. 
We prefer the slight shrinkage of the tolerance parameter 
because it is more effective and the significance level remains unchanged. 

The bootstrap test is realized as the function “bootstrapTest”, which has the following parameters:

• p is a two dimensional array of double. It should contain two way contingency table.
• n is the number of observations.
• alpha is the significance level.
• epsilon is the tolerance parameter e.
• nDirections is the number of random directions to search for a boundary point of H0.
  The number of random directions has a negative impact on the computation time. 
  The number should be set empirically. You can increase it gradually (100, 200, ...) 
  until the minimum tolerance parameter “minEps” does not change anymore. 
  For example, we would recommend to use 100 directions for 2x4 tables and 1000 directions for 4x5 tables.

• nBootstrapSamples is the number of bootstrap samples. The parameter should be at least 1000. 
  However, higher values lead to the better approximation generally.
  Usually it is not necessary to generate more than 10.000 bootstrap samples.

The bootstrap test needs considerable computation time. For example, it may need few minutes on the usual office computer.
